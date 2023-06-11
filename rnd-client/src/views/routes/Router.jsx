import {useStore} from "../../stores/StoreProvider";
import {observer} from "mobx-react-lite";
import GuestRouter from "./GuestRouter";
import LoggedRouter from "./LoggedRouter";

const Router = observer(() => {
  return (useStore().session.logged ? <LoggedRouter/> : <GuestRouter/>);
});

export default Router