using System.Linq;
using Example_04.Homework.FirstOrmLibrary;
using Example_04.Homework.Models;
using Example_04.Homework.Models.Interfaces;
using Example_04.Homework.SecondOrmLibrary;

namespace Example_04.Homework
{
    public class Adapter<TDbEntity> : IFirstOrm<TDbEntity> where TDbEntity : IDbEntity
    {
        public ISecondOrm SecondOrm;

        public Adapter(ISecondOrm secondOrm)
        {
            SecondOrm = secondOrm;
        }

        public void Create(TDbEntity entity)
        {
            if (entity is DbUserEntity)
                SecondOrm.Context.Users.Add(entity as DbUserEntity);
            else
                SecondOrm.Context.UserInfos.Add(entity as DbUserInfoEntity);
        }

        public TDbEntity Read(int id)
        {
            if (SecondOrm.Context.Users.Select(entity => entity.Id).Contains(id))
                return (TDbEntity)(IDbEntity)SecondOrm.Context.Users.FirstOrDefault(entity => entity.Id == id);

            return (TDbEntity)(IDbEntity)SecondOrm.Context.UserInfos.FirstOrDefault(entity => entity.Id == id);
        }

        public void Update(TDbEntity entity)
        {
            Delete(entity);
            Create(entity);
        }

        public void Delete(TDbEntity entity)
        {
            if (entity is DbUserEntity)
                SecondOrm.Context.Users.Remove(entity as DbUserEntity);
            else
                SecondOrm.Context.UserInfos.Remove(entity as DbUserInfoEntity);
        }
    }
}
