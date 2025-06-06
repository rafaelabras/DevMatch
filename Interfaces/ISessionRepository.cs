﻿using DevMatch.Dtos.SessionDto;
using DevMatch.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DevMatch.Interfaces
{
    public interface ISessionRepository
    {
        public Task<User> CarregarUser(User user);
        public ICollection<SessionResponseDto> ReturnSessionsDto(User user);
        public Task<SessionResponseDto> UpdateSession(User user, int id, CreateSessionDto session);
        public Task<SessionResponseDto> DeleteSession(User user, int id);

        public User VerificarUserSession(User user, int sessionId);
        public Task <Session> GetSession(int sessionId);

        public Task<SessionResponseDto> ParticiparDeUmaSession(User user, Session session);
    }
}
