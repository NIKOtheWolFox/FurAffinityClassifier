■これは何？
Fur Affinityから保存したファイルを分類するツールです。
ダウンロード機能はついていませんのでご注意ください。

■実行方法
インストールは不要です。
FurAffinityClassifier.App.Wpf.exeを実行してください。

■設定項目について
・フォルダー設定
移動元と移動先のフォルダーを、[選択]ボタンから選択してください。
入力欄を直接変更することはできません。

・振り分け設定
分類はFur AffinityのユーザーIDを基準にして行います。

「フォルダーがないときは作成する」は
IDに対応するフォルダーが見つからない場合の動作に影響します。
チェックされている場合、ユーザーIDと同じ名前のフォルダーを作成します。
チェックされていない場合、その画像の分類をスキップします。

Fur AffinityのユーザーIDと違う名前のフォルダーに分類したい場合、
「IDと異なるフォルダーに分類する」に設定を追加してください。

■動作概要
以下のフォルダー設定を行った場合
・移動元：C:\Users\foo\Downloads
・移動先：C:\Users\foo\Pictures
・移動元にあるファイル：[Submission ID].[FAのユーザーID]_[orig_name].jpg
ファイルは C:\Users\foo\Pictures\[FAのユーザーID] に分類されます。

■注意
Fur AffinityのユーザーIDはファイル名から判断します。
ファイルをリネームした場合は正常に動作しません。

■更新履歴
2020/04/26 0.1.0
初版リリース

2020/05/01 0.2.0
プロジェクト構成を見直し
GUIフレームワークをWPFに変更

2020/05/01 0.2.1
0.2.0でreadmeの不備があったため差し替えリリース

■作者に連絡を取りたい
Twitter : @NIKOtheWolFox
eメール : nikothewolfox@gmail.com

■著作権
(c) 2020 NIKO
このソフトウェアはMITライセンスにて公開します。
https://github.com/NIKOtheWolFox/FurAffinityClassifier/blob/master/LICENSE

■使用ライブラリ
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