■これは何？
Fur Affinityから保存したファイルを分類するツールです。
ダウンロード機能はついていませんのでご注意ください。

■実行方法
インストールは不要です。
FurAffinityClassifier.exeを実行してください。

■設定項目について
・フォルダー設定
移動元と移動先のフォルダーを、[選択]ボタンから選択してください。
入力欄を直接変更することはできません。

・振り分け設定
分類はFur AffinityのユーザーIDを基準にして行います。

「対応するフォルダーがない場合は作成する」は
IDに対応するフォルダーが見つからない場合の動作に影響します。
チェックされている場合、ユーザーIDと同じ名前のフォルダーを作成します。
チェックされていない場合、その画像の分類をスキップします。

Fur AffinityのユーザーIDと違う名前のフォルダーに分類したい場合、
「IDと異なるフォルダーに振り分ける」に設定を追加してください。

■動作概要
以下のフォルダー設定を行った場合
・移動元：C:\Users\foo\Downloads
・移動先：C:\Users\foo\Pictures
・移動元にあるファイル：[Submission ID].[FAのユーザーID]_[orig_name].jpg
ファイルは C:\Users\foo\Pictures\[FAのユーザーID] に分類されます。

■注意
Fur AffinityのユーザーIDはファイル名から判断します。
ファイルをリネームした場合は正常に動作しません。

■作者に連絡を取りたい
Twitter : @NIKOtheWolFox

■著作権
(c) 2020 NIKO
このソフトウェアはMITライセンスにて公開します。
https://github.com/NIKOtheWolFox/FurAffinityClassifier/blob/master/LICENSE

■使用ライブラリ
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