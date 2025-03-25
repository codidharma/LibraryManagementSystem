using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Infrastructure.Data.Dao;

namespace LMS.Modules.Membership.Infrastructure.Mapping.PatronAggregate;

internal static class PatronExtensions
{
    public static PatronDao ToDao(this Patron patron)
    {
        return new PatronDao()
        {
            Id = patron.Id.Value,
            Name = patron.Name.Value,
            Gender = patron.Gender.Value,
            DateOfBirth = patron.DateOfBirth.Value,
            Address = patron.Address.ToDao(),
            PatronType = new()
            {
                Id = patron.PatronType.Id,
                Name = patron.PatronType.Name
            },
            IdentityDocuments = patron.IdentityDocuments.Select(doc => doc.ToDao()).ToList()

        };
    }

    public static Patron ToDomainModel(this PatronDao dao)
    {
        Name name = new(dao.Name);
        Gender gender = new(dao.Gender);
        DateOfBirth dateOfBirth = new(dao.DateOfBirth);
        PatronType patronType = Enumeration.FromName<PatronType>(dao.PatronType.Name);
        Address address = dao.Address.ToDomainModel();
        List<Document> identityDocuments = dao.IdentityDocuments
            .Select(idoc => idoc.ToDomainModel()).ToList();

        Patron patron = Patron.Create(name, gender, dateOfBirth, address, patronType, identityDocuments);
        patron.SetEntityId(dao.Id);
        return patron;
    }
}
