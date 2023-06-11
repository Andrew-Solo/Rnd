import {useStore} from "stores/StoreProvider";
import {observer} from "mobx-react-lite";
import GuestRouter from "views/routes/GuestRouter";
import LoggedRouter from "views/routes/LoggedRouter";

const Router = observer(() => {
  return (useStore().session.logged ? <LoggedRouter/> : <GuestRouter/>);
});

export default Router