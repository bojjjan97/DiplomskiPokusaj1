using DiplomskiPokusaj1.DTO;
using DiplomskiPokusaj1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository.Interface
{
    public interface IUserRepository
    {
        public Task<ICollection<User>> GetAll(User userRequesting);
        public Task<User> Get(string id, User userRequesting);
        public Task<User> Create(CreateUserDTO user, User userRequesting);
        public Task<User> Update(string id, User user, User userRequesting);
        public Task<bool> Delete(string id, User userRequesting);
        public Task<LoginResponseDTO> Authenticate(LoginRequestDTO requestDTO);
    }
}
