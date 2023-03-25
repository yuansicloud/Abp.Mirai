namespace YSCloud.Abp.Mirai.Http.Utils.Internal
{
    internal static class MiraiHttpUtils
    {
        #region Guarantee

        /// <summary>
        ///     根据json判断这个json是否是正确的，否则抛出异常
        /// </summary>
        /// <param name="json"></param>
        /// <param name="appendix"></param>
        //internal static void EnsureSuccess(this string json, string appendix = null)
        //{
        //    var obj = JObject.Parse(json);

        //    if (obj.ContainsKey("code"))
        //    {
        //        var code = obj.Fetch("code");
        //        if (code != "0")
        //        {
        //            var message = $"原因: {json.OfErrorMessage()}";

        //            if (!appendix.IsNullOrEmpty())
        //                message += $"\r\n备注: {appendix}";
        //            else
        //                message += $"\r\n备注: {MiraiBot.Instance.ToJsonString()}";

        //            throw new InvalidResponseException(message);
        //        }
        //    }
        //}

        #endregion
    }
}
