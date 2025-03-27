using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Infrastructure.Data.Dao;

namespace LMS.Modules.Membership.Infrastructure.Mappings.PatronAggregate;

internal static class PatronExtensions
{
    public static PatronDao ToDao(this Patron patron)
    {
        return new PatronDao()
        {
            Id = patron.Id.Value,
            AccessId = patron.AccessId.Value,
            Name = patron.Name.Value,
            Gender = patron.Gender.Value,
            DateOfBirth = patron.DateOfBirth.Value,
            Email = patron.Email.Value,
            Address = patron.Address.ToDao(),
            PatronType = patron.PatronType.Name,
            IdentityDocuments = patron.IdentityDocuments.Select(doc => doc.ToDao()).ToList()

        };
    }

    public static Patron ToDomainModel(this PatronDao dao)
    {
        Name name = new(dao.Name);
        Gender gender = new(dao.Gender);
        DateOfBirth dateOfBirth = new(dao.DateOfBirth);
        Email email = new(dao.Email);
        PatronType patronType = Enumeration.FromName<PatronType>(dao.PatronType);
        Address address = dao.Address.ToDomainModel();
        List<Document> identityDocuments = dao.IdentityDocuments
            .Select(idoc => idoc.ToDomainModel()).ToList();
        AccessId accessId = new(dao.AccessId);

        Patron patron = Patron.Create(
            name,
            gender,
            dateOfBirth,
            email,
            address,
            patronType,
            identityDocuments,
            accessId);
        patron.SetEntityId(dao.Id);
        return patron;
    }
}
