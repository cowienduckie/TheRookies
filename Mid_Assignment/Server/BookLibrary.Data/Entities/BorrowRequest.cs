﻿using Common.Enums;

namespace BookLibrary.Data.Entities;

public class BorrowRequest : BaseEntity
{
    public RequestStatuses Status { get; set; }
    public int RequestedBy { get; set; }
    public User Requester { get; set; } = null!;
    public DateTime RequestedAt { get; set; }
    public int? ApprovedBy { get; set; }
    public User? Approver { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public ICollection<BorrowRequestDetail> BorrowRequestDetails { get; set; } = null!;
}