using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FurAffinityClassifier.CommonDotNetFramework.Datas;

namespace FurAffinityClassifier.CommonDotNetFramework.Models
{
    /// <summary>
    /// 分類機能
    /// </summary>
    public class ClassificationModel
    {
        #region Public Method

        public bool Execute(SettingData settingData)
        {
            bool result = true;

            try
            {
                var files = Directory.GetFiles(settingData.FromFolder);
                ////files.ToList().ForEach(f => Console.WriteLine(f));
                var files2 = files.Where(f => Regex.IsMatch(Path.GetFileName(f), @"[0-9]+\.[a-z0-9-~^.]{3,}_.*"));
                ////files2.ToList().ForEach(f => Console.WriteLine(f));
                foreach(var f in files2)
                {
                    var match = Regex.Match(f, @"[0-9]+\.(?<id>[a-z0-9-~^.]{3,}?)_.*");
                    var id = string.Empty;
                    if (match.Success)
                    {
                        id = match.Groups["id"].Value;
                        ////Console.WriteLine($"id from {f} is {match.Groups["id"].Value}");
                    }
                    else
                    {
                        ////Console.WriteLine($"cannot find id from {f}");
                        continue;
                    }

                    string foldername = id;
                    if (settingData.IdFolderMappings.Exists(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()))
                    {
                        foldername = settingData.IdFolderMappings.Where(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()).FirstOrDefault().FolderName;
                        ////Console.WriteLine($"ID({id}) is to {foldername}");
                    }

                    ////string folderpath = Path.Combine(settingData.ToFolder, foldername);
                    ////Console.WriteLine($"path {folderpath} exist? : {Directory.Exists(folderpath)}");
                    var foldersOnTo = Directory.GetDirectories(settingData.ToFolder);
                    var folderTo = string.Empty;
                    var matchedfolder = foldersOnTo.Where(ff => foldername.TrimEnd('.') == Path.GetFileName(ff).ToLower().Replace("_", string.Empty));
                    if (matchedfolder.Count() > 1)
                    {
                        Console.WriteLine($"multiple fromders for {foldername}");
                        continue;
                    }
                    else if (matchedfolder.Count() == 1)
                    {
                        folderTo = matchedfolder.First();
                        Console.WriteLine($"folder for ID({id}) is to {folderTo}");
                    }

                    if (string.IsNullOrEmpty(folderTo))
                    {
                        if (settingData.CreateFolderIfNotExist)
                        {
                            Console.WriteLine($"I have to create filder {foldername}");
                            Directory.CreateDirectory(Path.Combine(settingData.ToFolder, foldername.TrimEnd('.')));
                        }
                        else
                        {
                            Console.WriteLine("no folder, continue");
                            continue;
                        }
                    }
                }

                ////TODO:ファイルを移動先に移動する

                Console.WriteLine("＼(^o^)／");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = false;
            }

            return result;
        }

        #endregion
    }
}
