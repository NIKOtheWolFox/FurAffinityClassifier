# Fur Affinity Classifier
This application classify files that downloaded from Fur Affinity into ID-based folder.

[![Latest Release](https://img.shields.io/github/v/release/NIKOtheWolFox/FurAffinityClassifier)](https://github.com/NIKOtheWolFox/FurAffinityClassifier/releases)

## Requirements
.NET Framework 4.7.2 for GUI application.<br>
.NET Core 3.1 for Console application.

## How to use GUI application
GUI application is at "App.Wpf" folder.
1. Unzip released file.
1. Run "FurAffinityClassifier.App.Wpf.exe".
1. Set up as you wish. (setting will not save unless clicking "Save Setting")
1. Click "Execute" button.

## How to use Console application
Console application is at "App.Console" folder. This folder have 3 subfolders, choose one that suit for your environment.
1. Unzip released file.
1. Configure by "FurAffinityClassifier.App.Console --change-setting [param]". ([param] is written in "Console application reference" section)
1. Run "FurAffinityClassifier.App.Console.exe (FurAffinityClassifier.App.Console in macOS or Linux)".

## Console application reference
You can see this reference by running "FurAffinityClassifier.App.Console.exe --help".
```
FurAffinityClassifier.App.Console.exe : Execute classification.
FurAffinityClassifier.App.Console.exe --help : Show usage.
FurAffinityClassifier.App.Console.exe --version : Show version.
FurAffinityClassifier.App.Console.exe --show-setting : Show setting.
FurAffinityClassifier.App.Console.exe --change-setting [param] : Change setting. [param] are:
  from-folder=[source folder]
  to-folder=[destination folder]
  create-folder=[True|False]
  overwrite=[True|False]
  classify-as add [ID]=[folder]
  classify-as delete [ID]
```

## Setting
This application use a file "setting.json" in same folder as executable file to save setting.<br>
Content of file is:
```JSON:setting.json
{
    "FromFolder": "[folder contains files before classification]",
    "ToFolder": "[parent folder of ID-based folders]",
    "CreateFolderIfNotExist": [true | false],
    "OverwriteIfExist": [true | false],
    "ClassifyAsDatas": [
        {
            "Id": "[FA user ID]",
            "Folder": "[folder name, if you want to classify to different name folder]"
        },
        ...
    ]
}
```
GUI application create and update this file by clicking "Save Setting" button, but you can create it manually.<br>
Console application create and update this file by execute "FurAffinityClassifier --change-setting [param]", of course you can create it manually.<br>
If you make setting file by using this app, order of content can be differ from above example. This is derived from JSON processor library behavior.

## How application works
If you set up as:
```JSON:setting.json
{
    "FromFolder": "C:\\Users\\foo\\Downloads",
    "ToFolder": "C:\\Users\\foo\\Pictures",
    "CreateFolderIfNotExist": false,
    "OverwriteIfExist": false,
    "ClassifyAsDatas": [
        {
            "Id": "test",
            "Folder": "test_folder"
        }
    ]
}
```
and you have 2 files in C:\Users\foo\Downloads (Do not rename file, this application detect file by filename):
* 1234567890.test_bar.jpg
* 2345678901.test2_baz.jpg

Then you run this application, files are classified as below:
* 1234567890.test_bar.jpg : FA user ID is in ClassifyAsDatas, so this file will be moved to C:\Users\foo\Pictures\test_folder.
* 2345678901.test2_baz.jpg : FA user ID isn't in Classifyas Datas, so this file will be moved to C:\Users\foo\Pictures\test2.

## Copyright
(c) 2020 NIKO

## Contact to author
Twitter : [@NIKOtheWolFox](https://twitter.com/NIKOtheWolFox/)<br>
Fur Affinity : [NIKOtheWolFox](https://www.furaffinity.net/user/nikothewolfox/)

## License
![MIT License](https://img.shields.io/github/license/NIKOtheWolFox/FurAffinityClassifier)<br>
See [here](https://raw.githubusercontent.com/NIKOtheWolFox/FurAffinityClassifier/master/LICENSE) for more information.

## Plans
* Port to .NET 5
* Create multi platform GUI application by using Avalonia UI.
* * -> I tried but Avalonia DataGrid is difficult for me, it will be abandoned.

## Libraries usage
[Jil](https://github.com/kevin-montrose/Jil)<br>
(c) 2013-2019 Kevin Montrose<br>
[MIT License](https://github.com/kevin-montrose/Jil/blob/master/LICENSE)<br>

[NLog](https://nlog-project.org/)<br>
(c) 2004-2020 NLog Project - https://nlog-project.org/<br>
[BSD 3-Clause License](https://github.com/NLog/NLog/blob/dev/LICENSE.txt)<br>

[WindowsAPICodePack](https://github.com/contre/Windows-API-Code-Pack-1.1)<br>
(c) 2020 rpastric, contre, dahall<br>
[Custom License](https://github.com/contre/Windows-API-Code-Pack-1.1/blob/master/LICENSE)<br>

[MVVM Light Toolkit](http://www.mvvmlight.net/)<br>
(c) 2009-2018 Laurent Bugnion (GalaSoft)<br>
[MIT License](https://github.com/lbugnion/mvvmlight/blob/master/LICENSE)<br>

[ReactiveProperty](https://github.com/runceel/ReactiveProperty)<br>
(c) 2018 neuecc, xin9le, okazuki<br>
[MIT License](https://github.com/runceel/ReactiveProperty/blob/master/LICENSE.txt)

## Other things
The author is not an English native, so advices are welcome.