using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Memom.Service;

namespace MemomMvc52.Areas.MemomWeb.Models
{
    public class LoginApiInputModel
    {
        [Required]
        [Display(Name = "Username or Email")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }


    
    public class LoginApiAccount
    {
        private List<AuthToken> tokens;

        public LoginApiAccount()
        {
            this.tokens = new List<AuthToken>();
            this.UserAlbums = new List<ApiUserAlbum>();
            this.UserAlbumInstances = new List<ApiUserAlbumInstance>();
            this.UserRewards = new List<ApiUserReward>();
        }

        public int Key { get; set; }
        public System.Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public bool RequiresPasswordReset { get; set; }

        public List<AuthToken> Tokens
        {
            get { return tokens; }
            set { tokens = value; }
        }

        public ICollection<ApiUserAlbum> UserAlbums { get; set; }
        public ICollection<ApiUserAlbumInstance> UserAlbumInstances { get; set; }
        public ICollection<ApiUserReward> UserRewards { get; set; }

        
    }


    public class AuthToken
    {
        public AuthToken(string TokenName, string TokenValue)
        {
            this.TokenName = TokenName;
            this.TokenValue = TokenValue;
        }

        public string TokenName { get; set; }
        public string TokenValue { get; set; }
    }

    public class ApiUserAlbum
    {
        public int Key { get; set; }
        public string AlbumName { get; set; }
        public Nullable<int> PhotoCount { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string Description { get; set; }
    }

    public partial class ApiUserAlbumInstance
    {

        public int Key { get; set; }
        public int AlbumKey { get; set; }
        public string AlbumName { get; set; }
        public Nullable<int> Score { get; set; }
        public string PlayLogFileUploadStatus { get; set; }
 
    }

    public class ApiUserReward
    {
        public int Key { get; set; }
        public int RewardKey { get; set; }
        public Nullable<int> RewardStatus { get; set; }
        public string RewardType { get; set; }
        public string RewardName { get; set; }
        public string Level1Name { get; set; }
        public string Level2Name { get; set; }
    }
}