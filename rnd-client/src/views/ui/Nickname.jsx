import {Typography} from "@mui/material";
import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/StoreProvider";

const Nickname = observer(({name, ...props}) => {
  const username = useStore().session.user.name;
  const isUser = username === name;

  return (
    <Typography variant="caption" color={isUser ? "warning.main" : "text.secondary"} {...props}>
      {name}
    </Typography>
  )
});

export default Nickname