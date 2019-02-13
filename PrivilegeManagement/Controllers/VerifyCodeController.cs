using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PrivilegeManagement.Arguments;
using PrivilegeManagement.Results;
using PrivilegeManagement.VerifyCode;
using System;
using System.DrawingCore.Imaging;
using System.IO;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrivilegeManagement.Controllers
{
    [EnableCors("AllowCors")]
    [Route("api/privilege/[controller]/[action]")]
    [ApiController]
    public class VerifyCodeController : PrivilegeController
    {

        /// <summary>
        /// 数字验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<FileContentResult> NumberVerifyCode(ArguUserGuid arguUserGuid)
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
            await _redisCacheClient.Value.setSingle(arguUserGuid.Token, code,TimeSpan.FromSeconds(60));
            byte[] codeImage = VerifyCodeHelper.GetSingleObj().CreateByteByImgVerifyCode(code, 100, 40);
            return File(codeImage, @"image/jpeg");
        }

        /// <summary>
        /// 字母验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<FileContentResult> AbcVerifyCode(ArguUserGuid arguUserGuid)
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.AbcVerifyCode);
            await _redisCacheClient.Value.setSingle(arguUserGuid.Token, code, TimeSpan.FromSeconds(60));
            var bitmap = VerifyCodeHelper.GetSingleObj().CreateBitmapByImgVerifyCode(code, 100, 40);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);
            return File(stream.ToArray(), "image/png");
        }


        /// <summary>
        /// 混合验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<FileContentResult> MixVerifyCode(ArguUserGuid arguUserGuid)
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.MixVerifyCode);
            await _redisCacheClient.Value.setSingle(arguUserGuid.Token, code, TimeSpan.FromSeconds(60));
            var bitmap = VerifyCodeHelper.GetSingleObj().CreateBitmapByImgVerifyCode(code, 100, 40);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            return File(stream.ToArray(), "image/gif");
        }

        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        [HttpPost]
        public async Task<PrivilegeBaseResult> CheckVerifyCode([FromBody]ArguCheckVerifyCode arguCheckVerifyCode)
        {
            var keyValue = await _redisCacheClient.Value.GetOrDefaultSingle(arguCheckVerifyCode.Token);
            if (keyValue.Equals(arguCheckVerifyCode.VerifyCode))
            {
                return Successed(null);
            }
            else
            {
                return Failed(null);
            }
        }

    }
}
