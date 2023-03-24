using Abp.Mirai.Common.Data.Shared;
using Abp.Mirai.Common.Services;
using Abp.Mirai.Http.Infrastructure.Sessions;
using Manganese.Text;
using Newtonsoft.Json;

namespace Abp.Mirai.Http.Services
{
    public class MiraiHttpGroupManager : MiraiHttpCommonService, IMiraiGroupManager
    {
        public  async Task DeleteGroupAnnouncementAsync(string group, string fid, string? qq = null)
        {
            var payload = new
            {
                id = group,
                fid
            };

            await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.DelAnnouncement, HttpMethod.Post, payload, qq);
        }

        public async Task<IEnumerable<Announcement>> GetGroupAnnouncementAsync(string group, long offset = 0, long size = 10, string? qq = null)
        {
            var payload = new
            {
                id = group,
                offset,
                size
            };

            var response = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.GetAnnouncement, HttpMethod.Get, payload, qq);

            return JsonConvert.DeserializeObject<IEnumerable<Announcement>>(response.Fetch("data"));
        }

        public async Task<GroupSetting> GetGroupSettingAsync(string groupId, string? qq = null)
        {
            var payload = new
            {
                target = groupId
            };

            var response = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.GroupConfig, HttpMethod.Get, payload, qq);

            return JsonConvert.DeserializeObject<GroupSetting>(response.ToString());

        }

        public async Task<Member> GetMemberAsync(string memberQQ, string group, string? qq = null)
        {
            var payload = new
            {
                target = group,
                memberId = memberQQ
            };

            var response = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.MemberInfo, HttpMethod.Get, payload, qq);

            return JsonConvert.DeserializeObject<Member>(response.ToString());
        }

        public async Task KickAsync(string memberId, string group, string message = "", string? qq = null)
        {
            var payload = new
            {
                target = group,
                memberId,
                msg = message
            };

            await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.Kick, HttpMethod.Post, payload, qq);
        }

        public async Task LeaveAsync(string groupId, string? qq = null)
        {
            var payload = new
            {
                groupId
            };

            await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.Leave, HttpMethod.Post, payload, qq);
        }

        public async Task MuteAllAsync(string groupId, bool mute = true, string? qq = null)
        {
            var endpoint = mute ? HttpEndpoints.MuteAll : HttpEndpoints.UnmuteAll;

            var payload = new
            {
                groupId
            };

            await MiraiHttpApiRequester.RequestAsync(endpoint, HttpMethod.Post, payload, qq);
        }

        public async Task MuteAsync(string memberId, string group, int time, string? qq = null)
        {
            var payload = new
            {
                target = group,
                memberId,
                time
            };

            await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.Mute, HttpMethod.Post, payload, qq);
        }

        public async Task<Announcement> PublishGroupAnnouncementAsync(string group, string content, bool pinned = true, string? qq = null)
        {
            var setting = new AnnouncementSetting
            {
                Target = group,
                Content = content,
                Pinned = pinned
            };

            return await PublishGroupAnnouncementAsync(setting);
        }

        public async Task<Announcement> PublishGroupAnnouncementAsync(AnnouncementSetting announcementSetting, string? qq = null)
        {
            var response = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.PubAnnouncement, HttpMethod.Post, announcementSetting, qq);

            return JsonConvert.DeserializeObject<Announcement>(response.Fetch("data"));
        }

        public async Task SetEssenceMessageAsync(string messageId, string groupId, string? qq = null)
        {
            var payload = new
            {
                target = groupId,
                messageId
            };

            await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.SetEssence, HttpMethod.Post, payload, qq);

        }

        public async Task SetGroupSettingAsync(string groupId, GroupSetting setting, string? qq = null)
        {
            var payload = new
            {
                target = groupId,
                config = setting
            };

            await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.GroupConfig, HttpMethod.Post, payload, qq);
        }

        public async Task<Member> SetMemberInfoAsync(string memberQQ, string group, string card = null, string title = null, string? qq = null)
        {
            var payload = new
            {
                target = group,
                memberId = memberQQ,
                info = new
                {
                    name = card,
                    specialTitle = title
                }
            };

            await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.MemberInfo, HttpMethod.Post, payload, qq);

            return await GetMemberAsync(memberQQ, group);
        }

        public async Task UnMuteAsync(string memberId, string group, string? qq = null)
        {
            var payload = new
            {
                target = group,
                memberId
            };

            await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.Unmute, HttpMethod.Post, payload, qq);
        }
    }
}
