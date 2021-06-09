using Diploma.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diploma.Models
{
    public class ValueEntryHeader
    {
        public int ID { get; set; }

        //Возможные значения:План, Факт
        [Display(Name = "Тип")]
        public string EntryType { get; set; }

        [Display(Name = "Версия")]
        public int Version_ID { get; set; }

        [Display(Name = "Подразделение")]
        public int Structure_ID { get; set; }

        [Display(Name = "Статья")]
        public int BudgetItem_ID { get; set; }

        [Display(Name = "Дата учета")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]        
        public DateTime PostingDate { get; set; }

        [Display(Name = "Пользователь")]
        public string CreatedUserID { get; set; }

        [Display(Name ="В работе")]
        public bool IsWorking { get; set; }

        [ForeignKey("PostingDate")]
        public virtual Dates Dates { get; set; }

        [ForeignKey("Structure_ID")]
        public virtual Structure Structure { get; set; }

        [ForeignKey("BudgetItem_ID")]
        public virtual BudgetItem BudgetItem { get; set; }

        [ForeignKey("Version_ID")]
        public virtual BudgetVersion Version { get; set; }

        [ForeignKey("CreatedUserID")]
        public virtual ApplicationUser CreatedUser { get; set; }

        public virtual List<ValueEntryDetail> ValueEntryDetails { get; set; }

        //Конструктор по умлочанию
        public ValueEntryHeader() { }

        //Конструктор присваивания
        public ValueEntryHeader(ValueEntryHeader h)
        {

        }

    }

    //Класс фильтров, используется для сохранения фильтров для документов бюджета
    public class VEH_Filters
    {
        private int? VersionID { get; set; }

        private int? StructureID { get; set; }

        private int? ItemID { get; set; }

        //Функция для заполнения свойств класса
        public void FillFilters(int? Version, int? Structure, int? Item)
        {
            this.VersionID = Version;
            this.StructureID = Structure;
            this.ItemID = Item;
        }
        //Функции для получения свойств класса
        public int? GetVersion()
        {
            return this.VersionID;
        }

        public int? GetItem()
        {
            return this.ItemID;
        }

        public int? GetStructure()
        {
            return this.StructureID;
        }

        //Конструкторы класса фильтров
        public VEH_Filters() { }

        public VEH_Filters(int? Version,int? Structure,int? Item)
        {
            VersionID = Version;
            StructureID = Structure;
            ItemID = Item;
        }
    }
    
}