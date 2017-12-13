namespace MVCHomeWork.Models
{
    using MVCHomeWork.Models.Validate.Attribute;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人
    {
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        [JsonProperty("job")]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        [JsonProperty("name")]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [MobilePhoneValidate(ErrorMessage ="手機格式必須如0911-111111")]
        [JsonProperty("phone")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [JsonProperty("tellPhone")]
        public string 電話 { get; set; }
        [JsonIgnore]
        public bool 是否已刪除 { get; set; }
        [JsonIgnore]
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
