/****************************************************************************
 * Copyright (c) 2015 - 2022 liangxiegame UNDER MIT License
 * 
 * http://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

namespace QFramework
{
    /// <summary>
    /// singleton interface
    /// </summary>
    public interface ISingleton
    {
        /// <summary>
        /// Singleton initialization (classes that inherit the current interface need to implement this method)
        /// </summary>
        void OnSingletonInit();
    }
}