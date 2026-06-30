using MM.IT.Common.Models.Base.Interfaces;
using MM.IT.Common.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Base;

/// <summary>
/// Entity Model Nesnesi
/// </summary>
[Serializable]
public abstract class BaseEntityModel : IEntityModel
{

}

/// <summary>
/// Primary Key ile Generic Entity Model Nesnesi
/// </summary>
/// <typeparam name="TPrimaryKey">PrimaryKey</typeparam>
[Serializable]
public abstract class BaseEntityModel<TPrimaryKey> : BaseEntityModel, IEntityModel<TPrimaryKey>
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public TPrimaryKey Id { get; set; }
}

/// <summary>
/// TranslationModel Listesi ile Generic Entity Model Nesnesi
/// </summary>
/// <typeparam name="TTranslationModel">TranslationModel Nesnesi</typeparam>
[Serializable]
public abstract class BaseEntityModelWithTranslations<TTranslationModel> : BaseEntityModel, IEntityModelWithTranslations<TTranslationModel> where TTranslationModel : class, ITranslationEntityModel
{
    /// <summary>
    /// TranslationModel Listesi
    /// </summary>
    public ICollection<TTranslationModel> Translations { get; set; }
}

/// <summary>
/// Primary Key ve Translation Model Listesi ile Generic Entity Model Nesnesi
/// </summary>
/// <typeparam name="TTranslationModel">TranslationModel Nesnesi</typeparam>
/// <typeparam name="TPrimaryKey">PrimaryKey</typeparam>
[Serializable]
public abstract class BaseEntityModelWithTranslations<TTranslationModel, TPrimaryKey> : BaseEntityModel<TPrimaryKey>, IEntityModelWithTranslations<TTranslationModel> where TTranslationModel : class, ITranslationEntityModel
{
    /// <summary>
    /// TranslationModel Listesi
    /// </summary>
    public ICollection<TTranslationModel> Translations { get; set; }
}

/// <summary>
/// Aktif Translation Model ile Generic Entity Model Nesnesi
/// </summary>
/// <typeparam name="TTranslationModel">TranslationModel Nesnesi</typeparam>
[Serializable]
public abstract class BaseEntityModelWithCurrentTranslation<TTranslationModel> : BaseEntityModel, IEntityModelWithCurrentTranslation<TTranslationModel> where TTranslationModel : class, ITranslationEntityModel
{
    /// <summary>
    /// Constructor
    /// </summary>
    public BaseEntityModelWithCurrentTranslation()
    {
        CurrentTranslation = Activator.CreateInstance<TTranslationModel>();
    }

    /// <summary>
    /// Aktif TranslationModel Bilgisi
    /// </summary>
    public TTranslationModel CurrentTranslation { get; set; }
}

/// <summary>
/// Aktif Translation Model ve PrimaryKey ile Generic Entity Model Nesnesi
/// </summary>
/// <typeparam name="TTranslationModel">TranslationModel Nesnesi</typeparam>
/// <typeparam name="TPrimaryKey">PrimaryKey</typeparam>
[Serializable]
public abstract class BaseEntityModelWithCurrentTranslation<TTranslationModel, TPrimaryKey> : BaseEntityModel<TPrimaryKey>, IEntityModelWithCurrentTranslation<TTranslationModel> where TTranslationModel : class, ITranslationEntityModel
{
    /// <summary>
    /// Constructor
    /// </summary>
    public BaseEntityModelWithCurrentTranslation()
    {
        CurrentTranslation = Activator.CreateInstance<TTranslationModel>();
    }

    /// <summary>
    /// Aktif TranslationModel Bilgisi
    /// </summary>
    public TTranslationModel CurrentTranslation { get; set; }
}

/// <summary>
/// IAuditableEntity şartlarını barındıran Abstract Entity Model Nesnesi.
/// PrimaryKey, Tenant bilgisi ve Audit Bilgisi barındırması gereken Entity'ler bu nesneden türetilir.
/// </summary>
/// <typeparam name="TPrimaryKey">Object: Entity Primary Key</typeparam>
public abstract class BaseAuditableEntityModel<TPrimaryKey> : BaseEntityModel<TPrimaryKey>
{
    /// <summary>
    /// Oluşturulma tarihi bilgisini saklar.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// UserEntity: Erişim için yapılan tanımlama
    /// </summary>
    public UserViewModel CreatedByUser { get; set; }

    /// <summary>
    /// Düzenlenme tarihi bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// UserEntity: Erişim için yapılan tanımlama
    /// </summary>
    public UserViewModel UpdatedByUser { get; set; }
}
