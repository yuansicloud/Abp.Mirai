using Abp.Mirai.Common.Data.Shared;

namespace Abp.Mirai.Common.Services
{
    /// <summary>
    /// 群管理器
    /// </summary>
    public interface IMiraiGroupManager
    {
        #region Mute

        /// <summary>
        ///     禁言某群员
        /// </summary>
        /// <param name="memberId">禁言对象的QQ号</param>
        /// <param name="group">禁言对象所在的群号</param>
        /// <param name="time">禁言的时长</param>
        Task MuteAsync(string memberId, string group, int time, string? qq = null);

        #endregion

        #region UnMute

        /// <summary>
        ///     取消禁言
        /// </summary>
        /// <param name="memberId">取消禁言对象的QQ号</param>
        /// <param name="group">取消禁言对象所在的群号</param>
       Task UnMuteAsync(string memberId, string group, string? qq = null);

        #endregion

        #region Kick

        /// <summary>
        ///     踢出某群员
        /// </summary>
        /// <param name="memberId">踢出对象的QQ号</param>
        /// <param name="group">踢出对象所在的群号</param>
        /// <param name="message">踢出的原因</param>
        Task KickAsync(string memberId, string group, string message = "", string? qq = null);

        #endregion

        #region Leave

        /// <summary>
        ///     bot退出某群
        /// </summary>
        /// <param name="groupId">要退出的群号</param>
        Task LeaveAsync(string groupId, string? qq = null);

        #endregion

        #region MuteAll

        /// <summary>
        ///     全体禁言
        /// </summary>
        /// <param name="groupId">目标群号</param>
        /// <param name="mute">是否禁言。 false为解除禁言，true为禁言</param>
        Task MuteAllAsync(string groupId, bool mute = true, string? qq = null);

        #endregion

        #region Essence

        /// <summary>
        ///     设置精华消息
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <param name="groupId">目标群号</param>
        Task SetEssenceMessageAsync(string messageId, string groupId, string? qq = null);

        #endregion

        #region GroupSetting

        /// <summary>
        ///     获取群设置
        /// </summary>
        /// <param name="groupId">目标群号</param>
        /// <returns></returns>
        Task<GroupSetting> GetGroupSettingAsync(string groupId, string? qq = null);


        /// <summary>
        ///     修改群设置
        /// </summary>
        /// <param name="groupId">目标群号</param>
        /// <param name="setting">群设置</param>
        Task SetGroupSettingAsync(string groupId, GroupSetting setting, string? qq = null);

        #endregion

        #region MemberInfo

        /// <summary>
        ///     获取群员
        /// </summary>
        /// <param name="memberQQ">目标的QQ号</param>
        /// <param name="group">目标群号</param>
        /// <returns></returns>
        Task<Member> GetMemberAsync(string memberQQ, string group, string? qq = null);

        /// <summary>
        ///     修改群员设置,需要相关的权限
        /// </summary>
        /// <param name="memberQQ">目标的QQ号</param>
        /// <param name="group">目标群号</param>
        /// <param name="card">群名片, 需要管理员权限</param>
        /// <param name="title">群头衔, 需要群主权限</param>
        /// <returns></returns>
        Task<Member> SetMemberInfoAsync(string memberQQ, string group, string card = null,
            string title = null, string? qq = null);

        #endregion

        #region Anno

        /// <summary>
        /// 获取指定群公告列表
        /// </summary>
        /// <param name="group">目标群号</param>
        /// <param name="offset">分页参数</param>
        /// <param name="size">分页参数，默认10</param>
        /// <returns></returns>
        Task<IEnumerable<Announcement>> GetGroupAnnouncementAsync(string group, long offset = 0, long size = 10, string? qq = null);

        /// <summary>
        /// 向指定群发布群公告
        /// </summary>
        /// <param name="group">目标群号</param>
        /// <param name="content">公告内容</param>
        /// <param name="pinned">是否置顶</param>
        /// <returns></returns>
        Task<Announcement> PublishGroupAnnouncementAsync(string group, string content, bool pinned = true, string? qq = null);

        /// <summary>
        /// 向指定群发布群公告
        /// </summary>
        /// <param name="announcementSetting">群公告设置</param>
        /// <returns></returns>
        Task<Announcement> PublishGroupAnnouncementAsync(AnnouncementSetting announcementSetting, string? qq = null);

        /// <summary>
        /// 删除指定群中一条公告
        /// </summary>
        /// <param name="group">目标群号</param>
        /// <param name="fid">群公告唯一id</param>
        /// <returns></returns>
        Task DeleteGroupAnnouncementAsync(string group, string fid, string? qq = null);

        #endregion

    }
}
