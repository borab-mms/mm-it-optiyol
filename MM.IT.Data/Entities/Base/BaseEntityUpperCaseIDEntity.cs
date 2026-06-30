using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.Base;


    /// <summary>
    /// IEntity şartlarını barındıran Abstract Entity Nesnesi.
    /// Sadece PrimaryKey barındırması gereken Entity'ler bu nesneden türetilir.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Object: Entity Primary Key</typeparam>
    public abstract class BaseEntityUpperCaseIDEntity<TPrimaryKey> : IEntityUpperCaseIDEntity<TPrimaryKey>//, ISoftDeletableEntity
    {
        /// <summary>
        /// PrimaryKey bilgisini saklar.
        /// </summary>
        [Key]
        public virtual TPrimaryKey ID { get; set; }

        /// <summary>
        /// Oluşturulma tarihi bilgisini saklar.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
