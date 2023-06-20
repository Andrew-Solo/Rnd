import React, {createContext, ReactNode, useContext} from "react";
import Store from "./Store";

export const store = new Store();
const StoreContext = createContext(store);
export const useStore = () => useContext(StoreContext);
export default function StoreProvider({children}: {children: ReactNode}) {
  return(
    // @ts-ignore
    <StoreContext.Provider value={store}>
      {children}
    </StoreContext.Provider>
  )
}

