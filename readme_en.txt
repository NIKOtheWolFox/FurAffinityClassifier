■What't that?
A tool to classify files downloaded from Fur Affinity.
No download function included.

■How to execute
No installation required.
Run FurAffinityClassifier.exe.

■About setting
*** Currently Japanese GUI only
- Folder setting
Choose source and destination Folder by using [選択] button.
You cannot input folder path.

- Classification setting
Classification is based on Fur Affinity user ID.

"対応するフォルダーがない場合は作成する" will effect if no folders were found.
If it checked, this app will create a folder.
If it not checked, classification of the file will be skipped.

If you want to put the file to a folder that differ from ID,
please add "IDと異なるフォルダーに振り分ける" settings.

■How app works
If you set Folder setting as below:
以下のフォルダー設定を行った場合
・source：C:\Users\foo\Downloads
・destination：C:\Users\foo\Pictures
・file in source folder：[Submission ID].[FA_user_ID]_[orig_name].jpg
File will be put in C:\Users\foo\Pictures\[FA_user_ID].

■Caution
This app detect Fur Affinity user ID via filename.
If you rename files before execution, app cannot classify renamed files.

■Contact
Twitter : @NIKOtheWolFox

■Copyright
(c) 2020 NIKO
This software is released under MIT License
https://github.com/NIKOtheWolFox/FurAffinityClassifier/blob/master/LICENSE

■Library Usage
log4net
(c) 2004-2017 The Apache Software Foundation
Apache License 2.0
http://logging.apache.org/log4net/license.html

WindowsAPICodePack
(c) 2020 rpastric, contre, dahall
Custom License
https://github.com/contre/Windows-API-Code-Pack-1.1/blob/master/LICENSE

Json.NET
(c) 2008 James Newton-King
MIT License
https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md