using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Base.Interfaces;

/// <summary>
/// Entity Model Interface Nesnesi
/// Tüm Entity Model'ler bu interface'den türemelidir.
/// </summary>
public interface IEntityModel
{

}

/// <summary>
/// Primary Key ile Generic Entity Model Interface Nesnesi
/// Id içerikli tüm Entity Model'ler bu interface'den türemelidir.
/// </summary>
/// <typeparam name="TPrimaryKey">Primary Key</typeparam>
public interface IEntityModel<TPrimaryKey> : IEntityModel
{
    /// <summary>
    /// Primary Key Bilgisi
    /// </summary>
    public TPrimaryKey Id { get; set; }
}

/// <summary>
/// Translation Entity Model Interface Nesnesi
/// Tüm TranslationEntity Modelleri bu interface'den türemelidir.
/// </summary>
public interface ITranslationEntityModel : IEntityModel
{
    /// <summary>
    /// Dil Id Bilgisi. LanguageEntity.Id
    /// </summary>
    public int LanguageId { get; set; }
}

/// <summary>
/// Primary Key ile Generic Translation Entity Model Interface Nesnesi
/// Id içerikli Tüm TranslationEntity Modelleri bu interface'den türemelidir.
/// </summary>
/// <typeparam name="TPrimaryKey">Primary Key</typeparam>
public interface ITranslationEntityModel<TPrimaryKey> : ITranslationEntityModel
{
    /// <summary>
    /// PrimaryKey Bilgisi
    /// </summary>
    public TPrimaryKey Id { get; set; }
}

/// <summary>
/// Aktif Translation Model içeren Generic Entity Model Interface Nesnesi    
/// </summary>
/// <typeparam name="TTranslationModel">Translation Model Nesnesi</typeparam>
public interface IEntityModelWithCurrentTranslation<TTranslationModel> : IEntityModel
{
    /// <summary>
    /// Aktif Dil Bilgisi
    /// </summary>
    public TTranslationModel CurrentTranslation { get; set; }
}

/// <summary>
/// Translation Model Listesi içeren Generic Entity Model Interface Nesnesi
/// </summary>
/// <typeparam name="TTranslationModel">Translation Model Nesnesi</typeparam>
public interface IEntityModelWithTranslations<TTranslationModel> : IEntityModel
{
    /// <summary>
    /// Translation Model Listesi
    /// </summary>
    public ICollection<TTranslationModel> Translations { get; set; }
}

/// <summary>
/// Entity Oluşturma Modeller için Kullanılacak Entity Model Interface Nesnesi
/// </summary>
public interface ICreateEntityModel : IEntityModel { }

/// <summary>
/// Entity Düzenleme Modeller için Kullanılacak Entity Model Interface Nesnesi
/// </summary>
/// <typeparam name="TPrimaryKey">Primary Key</typeparam>
public interface IUpdateEntityModel<TPrimaryKey> : IEntityModel
{
    /// <summary>
    /// Primay Key Bilgisi
    /// </summary>
    public TPrimaryKey Id { get; set; }
}