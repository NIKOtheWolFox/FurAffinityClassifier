■What't that?
A tool to classify files downloaded from Fur Affinity.
No download function included.

■How to execute
No installation required.
Run FurAffinityClassifier.App.Wpf.exe.

■About setting
*** Currently Japanese GUI only
- Folder setting
Choose source and destination Folder by using [選択] button.
You cannot input folder path.

- Classification setting
Classification is based on Fur Affinity user ID.

"Create folder if not exist" will effect if no folders were found.
If it checked, this app will create a folder.
If it not checked, classification of the file will be skipped.

If you want to put the file to a folder that differ from ID,
please add "Classify as" settings.

■How app works
If you set Folder setting as below:
・source：C:\Users\foo\Downloads
・destination：C:\Users\foo\Pictures
・file in source folder：[Submission ID].[FA_user_ID]_[orig_name].jpg
File will be put in C:\Users\foo\Pictures\[FA_user_ID].

■Caution
This app detect Fur Affinity user ID via filename.
If you rename files before execution, app cannot classify renamed files.

■Change log
2020/04/26 0.1.0
Initial release.

2020/05/01 0.2.0
Renew projects associatios.
Change GUI Framework to WPF.

2020/05/01 0.2.1
Fix missed updated of readme at 0.2.0.

■Contact
Twitter : @NIKOtheWolFox
e-mail : nikothewolfox@gmail.com

■Copyright
(c) 2020 NIKO
This software is released under MIT License
https://github.com/NIKOtheWolFox/FurAffinityClassifier/blob/master/LICENSE

■Library Usage
Json.NET
(c) 2008 James Newton-King
MIT License
https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md

NLog
(c) 2004-2020 NLog Project - https://nlog-project.org/
BSD 3-Clause License
https://github.com/NLog/NLog/blob/dev/LICENSE.txt

WindowsAPICodePack
(c) 2020 rpastric, contre, dahall
Custom License
https://github.com/contre/Windows-API-Code-Pack-1.1/blob/master/LICENSE

MVVM Light Toolkit
(c) 2009-2018 Laurent Bugnion (GalaSoft)
MIT License
https://github.com/lbugnion/mvvmlight/blob/master/LICENSE

ReactiveProperty
(c) 2018 neuecc, xin9le, okazuki
MIT License
https://github.com/runceel/ReactiveProperty/blob/master/LICENSE.txt