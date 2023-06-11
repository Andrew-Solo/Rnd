import {createContext, useContext} from "react";
import Store from "./Store";

export const useStore = (): Store => useContext(StoreContext);
export default function StoreProvider({children}) {
  return(
    <StoreContext.Provider value={new Store()}>
      {children}
    </StoreContext.Provider>
  )
}

const StoreContext = createContext(null);