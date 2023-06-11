import React, {createContext, ReactNode, useContext} from "react";
import Store from "./Store";

export const useStore = () => useContext(StoreContext);
export default function StoreProvider({children}: {children: ReactNode}) {
  return(
    // @ts-ignore
    <StoreContext.Provider value={new Store()}>
      {children}
    </StoreContext.Provider>
  )
}

const StoreContext = createContext(null);