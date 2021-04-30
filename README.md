# Fur Affinity Classifier
This application classify files that downloaded from Fur Affinity into ID-based folder.

[![Latest Release](https://img.shields.io/github/v/release/NIKOtheWolFox/FurAffinityClassifier)](https://github.com/NIKOtheWolFox/FurAffinityClassifier/releases)

## Requirements
.NET 5

## How to use
1. Unzip released file.
1. Run "FurAffinityClassifier.exe".
1. Set up as you wish. (setting will not save unless clicking "Save Setting")
1. Click "Execute" button.

## Settings
This application use a file "settings.json" in same folder as executable file to save setting.<br>
Content of file is:
```JSON:settings.json
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
Application create and update this file by clicking "Save Setting" button, but you can create it manually.<br>
If you make setting file by using this app, order of content can be differ from above example. This is derived from JSON processor library behavior.

## How application works
If you set up as:
```JSON:settings.json
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
* Create multi platform GUI application by using Avalonia UI. -> It's hard for me, it will be abandoned.

## Libraries usage
[NLog](https://nlog-project.org/)<br>
(c) 2004-2020 NLog Project - https://nlog-project.org/<br>
[BSD 3-Clause License](https://github.com/NLog/NLog/blob/dev/LICENSE.txt)<br>

[Ookii.Dialogs.Wpf](https://github.com/ookii-dialogs/ookii-dialogs-wpf)<br>
(c) 2009-2020 Ookii Dialogs Contributors
[BSD 3-Clause License](https://github.com/ookii-dialogs/ookii-dialogs-wpf/blob/master/LICENSE)

[Prism](https://github.com/PrismLibrary/Prism)<br>
(c) Brian Lagunas, Dan Siegel<br>
[MIT License](https://github.com/PrismLibrary/Prism/blob/master/LICENSE)

[Unity.Container](https://github.com/unitycontainer/unity)<br>
(c) .NET Foundation and Contributors<br>
[Apache License 2.0](https://github.com/unitycontainer/unity/blob/master/LICENSE)

[ReactiveProperty](https://github.com/runceel/ReactiveProperty)<br>
(c) 2018 neuecc, xin9le, okazuki<br>
[MIT License](https://github.com/runceel/ReactiveProperty/blob/master/LICENSE.txt)

[StyleCop.Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)<br>
(c) 2015 Tunnel Vision Laboratories, LLC<br>
[MIT License](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/LICENSE)

## Other things
The author is not an English native, so advices are welcome.