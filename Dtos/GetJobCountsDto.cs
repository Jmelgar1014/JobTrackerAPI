using System;

namespace JobTrackerApi.Dtos;

public class GetJobCountsDto
{
    public int Total {get; set;}

    public int Applied {get; set;}

    public int Interviews {get; set;}

    public int Offers {get; set;}

    public int Rejected {get; set;}
}
