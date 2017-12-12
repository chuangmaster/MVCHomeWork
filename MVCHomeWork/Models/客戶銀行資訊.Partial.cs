namespace MVCHomeWork.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶銀行資訊MetaData))]
    public partial class 客戶銀行資訊
    {
    }
    
    public partial class 客戶銀行資訊MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        [JsonProperty("bankName")]
        public string 銀行名稱 { get; set; }
        [Required]
        [JsonProperty("swiftCode")]
        public int 銀行代碼 { get; set; }
        [JsonProperty("code")]
        public Nullable<int> 分行代碼 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        [JsonProperty("account")]
        public string 帳戶名稱 { get; set; }
        
        [StringLength(20, ErrorMessage="欄位長度不得大於 20 個字元")]
        [Required]
        [JsonProperty("accountNo")]
        public string 帳戶號碼 { get; set; }
        [JsonIgnore]
        public bool 是否已刪除 { get; set; }
        [JsonIgnore]
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
