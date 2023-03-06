using Abp.Mirai.Common.Data.Messages;
using Abp.Mirai.Common.Data.Messages.Concretes;
using Abp.Mirai.Http.Services;
using Shouldly;
using Xunit;

namespace Abp.Mirai.Http.Tests.Services
{
    public class MessageManagerTests : AbpMiraiHttpTestBase
    {
        private readonly MiraiHttpMessageManager _messageManager;

        public MessageManagerTests()
        {
            _messageManager = GetRequiredService<MiraiHttpMessageManager>();
        }

        [Fact]
        public async Task Should_Get_OpenId_And_SessionKey()
        {
            var msg = new MessageChain() {
            new PlainMessage("123")
            };

            var result = await _messageManager.SendFriendMessageAsync("1424494142", msg, "2467703805");
            
            result.ShouldNotBeNull();
            //result.ErrorCode.ShouldBe(0);
            //result.ErrorMessage.ShouldBeNull();
            
            //result.OpenId.ShouldNotBeEmpty();
            //result.SessionKey.ShouldNotBeEmpty();
        }
    }
}