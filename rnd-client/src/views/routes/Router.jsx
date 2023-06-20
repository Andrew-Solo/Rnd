import {useStore} from "../../stores/StoreProvider";
import {observer} from "mobx-react-lite";
import GuestRouter from "./GuestRouter";
import AppRouter from "./AppRouter";

const Router = observer(() => useStore().session.logged ? <AppRouter/> : <GuestRouter/>);

export default Router