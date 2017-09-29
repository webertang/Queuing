
namespace RDTools.Entity
{
    /// <summary>
    /// 实体状态枚举
    /// </summary>
    public enum EntityState
    {
        [Enum("未改变")]
        Unchange,

        [Enum("已改变")]
        Changed,

        [Enum("新增")]
        Add,

        [Enum("删除")]
        Delete
    }
}
