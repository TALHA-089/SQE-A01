namespace Testing;

/// <summary>
/// MC/DC tests for OrderSearchManager.FindOrder
/// Three single-condition decisions (each if-branch):
///   D1: searchType == "Linear"        → true / false
///   D2: searchType == "Binary"        → true / false
///   D3: searchType == "Interpolation" → true / false
/// MC/DC for each = branch coverage (T + F for each condition).
/// </summary>
[TestFixture]
public class OrderSearchManagerTests
{
    private OrderSearchManager _manager;
    private Order[] _sortedOrders;

    [SetUp]
    public void Setup()
    {
        _manager = new OrderSearchManager();
        _sortedOrders = new[]
        {
            new Order(10, "Alice"),
            new Order(20, "Bob"),
            new Order(30, "Charlie")
        };
    }

    // ── D1 true: "Linear" branch taken ──

    [Test]
    public void FindOrder_LinearSearch_ReturnsCorrectIndex()
    {
        int result = _manager.FindOrder(20, _sortedOrders, "Linear");

        Assert.That(result, Is.EqualTo(1));
    }

    // ── D2 true: "Binary" branch taken ──

    [Test]
    public void FindOrder_BinarySearch_ReturnsCorrectIndex()
    {
        int result = _manager.FindOrder(20, _sortedOrders, "Binary");

        Assert.That(result, Is.EqualTo(1));
    }

    // ── D3 true: "Interpolation" branch taken ──

    [Test]
    public void FindOrder_InterpolationSearch_ReturnsCorrectIndex()
    {
        int result = _manager.FindOrder(20, _sortedOrders, "Interpolation");

        Assert.That(result, Is.EqualTo(1));
    }

    // ── All branches false: unknown search type → returns -1 ──

    [Test]
    public void FindOrder_UnknownSearchType_ReturnsNegativeOne()
    {
        int result = _manager.FindOrder(20, _sortedOrders, "Unknown");

        Assert.That(result, Is.EqualTo(-1));
    }
}
