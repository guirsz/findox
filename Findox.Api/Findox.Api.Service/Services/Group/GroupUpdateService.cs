﻿using Findox.Api.Domain.Entities;
using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Repositories;
using Findox.Api.Domain.Interfaces.Services.Group;
using Findox.Api.Domain.Requests;

namespace Findox.Api.Service.Services.Group
{
    public class GroupUpdateService : IGroupUpdateService
    {
        private readonly IGroupRepository groupRepository;

        public GroupUpdateService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<(int groupId, string message)> RunAsync(int id, GroupUpdateRequest request, int requestedBy)
        {
            var groupEntity = await groupRepository.GetAsync(id);

            if (groupEntity == null || groupEntity.GroupId == 0)
            {
                return (0, ApplicationMessages.InvalidData);
            }

            var groupNameInUse = await GroupNameInUse(groupEntity, request);
            if (groupNameInUse)
            {
                return (0, ApplicationMessages.GroupNameInUse);
            }

            groupEntity.UpdatedDate = DateTime.Now;
            groupEntity.UpdatedBy = requestedBy;
            groupEntity.GroupName = request.GroupName;
            groupEntity.Deleted = false;

            await groupRepository.UpdateAsync(groupEntity);

            return (id, ApplicationMessages.UpdatedSuccessfully);
        }

        private async Task<bool> GroupNameInUse(GroupEntity groupEntity, GroupUpdateRequest request)
        {
            if (groupEntity.GroupName != request.GroupName)
            {
                var groupByName = await groupRepository.GetByName(request.GroupName);
                if (groupByName != null && groupByName.GroupId > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
