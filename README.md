MaskHeleprForNGUIAtlas

## 参考

https://github.com/keijiro/unity-alphamask

こちらで紹介されているテクニックを応用して
NGUI用のAtlasでも使いやすいようにしてみました。

## ポイント

* Atlasを元にマスク画像を生成できるEditorWindowを追加
* テクスチャ画像の半透明部分のサポート
* NGUIのtintColorなどもサポート

## 使用例

* NGUIでAtlas画像を生成しておく (Atlas画像は正方形になるように調整しておくこと)
* Atlas画像のTexture設定をTextureType -> Advancedにして、Read/Write Enabledをチェック、Mipmapはオフにしておく
* Window -> MaskTextureGeneratorを開き、Atlas画像を指定して、Gen Maskボタンを押すと同じディレクトリに末尾がMask.pngとなる画像が生成される
* Atlas画像に対応するマテリアルを選択して、ShaderをSpriteWithMaskに設定。Atlas画像とAtlasのマスク画像を指定
* Atlas画像のFormatをRGB Compression PVRTC 4bitにする
* Atlasのマスク画像のFormatをRGB Compression PVRTC 4bitにする

これにより、元々4MBあった1024x1024のAtlas画像が0.5MBx2になり、1/4のサイズに。

