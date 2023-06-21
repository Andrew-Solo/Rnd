import {Avatar, Box, Button, Typography} from "@mui/material";
import {observer} from "mobx-react-lite";
import {store} from "../../../stores/Store";

const AccountBanner = observer(() => {
  const user = store.session.user;

  return (
    <Button href="/account" variant="text" color="neutral" sx={{height: 80, padding: 0}}>
      <Box height={1} width={1} display="flex" gap="8px" justifyContent="center" alignItems="center"
           sx={{background: "rgba(255, 255, 255, 0.1)"}}>
        <Typography variant="h4">
          {user.title ?? user.name}
        </Typography>
        <Avatar alt={user.name} src={user.image}/>
      </Box>
    </Button>
  )
});

export default AccountBanner