using System;
using JobTrackerApi.Dtos;

namespace JobTrackerApi.Models;

public class ResponseMetaData<T>
{
    public List<T> Items {get;set;} = [];

    public int Page {get;set;}

    public int PageSize {get;set;}

    public int TotalCount {get;set;}
    
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public bool HasPreviousPage => Page > 1;

    public bool HasNextPage => Page < TotalPages;
}
