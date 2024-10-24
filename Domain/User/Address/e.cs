namespace Domain.User.Address;

public record Province(string Name, List<Counties> Counties);

public record Counties(string Name, List<Districts> Districts);

public record Districts(string Name, List<Cities> Cities);

public record Cities(string Name);