import {observer} from "mobx-react-lite";
import GuestRouter from "./GuestRouter";
import AppRouter from "./AppRouter";
import {store} from "../../stores/Store";

const Router = observer(() => store.session.logged ? <AppRouter/> : <GuestRouter/>);

export default Router