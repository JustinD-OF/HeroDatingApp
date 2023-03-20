using AutoMapper;
using AutoMapper.QueryableExtensions;
using HeroDatingApp.Controllers;
using HeroDatingApp.DTOs;
using HeroDatingApp.Entities;
using HeroDatingApp.Helpers;
using HeroDatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeroDatingApp.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParameters messageParams)
        {
            var query = _context.Messages.OrderByDescending(x => x.MessageSent).AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(user => user.RecipientUsername == messageParams.Username
                                        && user.RecipientDeleted == false),
                "Outbox" => query.Where(user => user.SenderUsername == messageParams.Username
                                        && user.SenderDeleted == false),
                _ => query.Where(user => user.RecipientUsername == messageParams.Username 
                                        && user.RecipientDeleted == false && user.DateRead == null)
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = await _context.Messages
                .Include(user => user.Sender).ThenInclude(photos => photos.Photos)
                .Where(
                    message => 
                        message.RecipientUsername == currentUsername && message.RecipientDeleted == false
                        && message.SenderUsername == recipientUsername
                        || message.RecipientUsername == recipientUsername && message.SenderDeleted == false
                        && message.SenderUsername == currentUsername
                )
                .OrderBy(message => message.MessageSent)
                .ToListAsync();

            var unreadMessages = messages.Where( message => message.DateRead == null && message.RecipientUsername == currentUsername).ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in messages)
                {
                    message.DateRead = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}