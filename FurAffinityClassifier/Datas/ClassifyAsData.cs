using System;

namespace FurAffinityClassifier.Datas
{
    /// <summary>
    /// IDと異なるフォルダーに分類する設定のデータ
    /// </summary>
    public class ClassifyAsData : IEquatable<ClassifyAsData>
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public ClassifyAsData()
        {
            Id = string.Empty;
            Folder = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// フォルダー
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// <inheritdoc cref="IEquatable{T}.Equals(T?)"/>
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(ClassifyAsData other)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                return Id == other.Id && Folder == other.Folder;
            }
        }

        /// <summary>
        /// <inheritdoc cref="object.Equals(object?)"/>
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object other)
        {
            return Equals(other as ClassifyAsData);
        }

        /// <summary>
        /// <inheritdoc cref="object.GetHashCode"/>
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Folder.GetHashCode();
        }
    }
}
