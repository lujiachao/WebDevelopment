using System;
using System.Collections.Generic;
using System.Text;

namespace WD.Common.WDEnum
{
    public enum EnumErrorStatus
    {
        APIError = 4010,
        APIArgumentError = 4011,
        APIIPAddressNoPermission = 4012,
        APITokenNotFound = 4013,
        APITokenNotFill = 4014,
        APITokenOverdue = 4015,
        APITokenOtherMistakes = 4016,
        MySQLError = 4030,
        MQError = 4040,

        AdminError = 4100,
        AdminRegisterCodeError = 4101,
        AdminRegisterUserNameError = 4102,
        AdminRegisterPasswordError = 4103,
        AdminLoginUserNameError = 4104,
        AdminLoginPasswordError = 4105,
        AdminLoginEnableError = 4106,
        AdminModifyEnable = 4107,
        NotSuperAdministrator = 4108,

        TicketError = 4200,
        TicketModifyArgumentError = 4201,
        MerchantError = 4300,
        MerchantModifyError = 4301,
        MerchantLoginError = 4302,
        MerchantModifyTradeError = 4303,
        MerchantModifyJurisdictionError = 4304,
        MerchantModifyStatus = 4305,
        MerchantModifyImage = 4306,
        ThemeError = 4400,
        ThemeModifyError = 4401,
        ThemeInsertError = 4403,
        ThemePayError = 4402,
        HeaderModifyError = 4500,
        ImageDeleteError = 4600,
        ImageLogoNotExist = 4601,
        ImageBackNotExist = 4602,


        SQLError = 5000,
        ListConditionCountError = 5001,

        SystemError = 6000,
        EncryptionRSAError = 6001,

        RSACheckingError = 7000,
        GuidError = 7001,
        SignError = 7002,
        SignIncorrect = 7003,

        IdCatchDetailError = 8000,
        DownloadFileError = 8100,

        BeyondLimitTime = 9000,
    }
}
