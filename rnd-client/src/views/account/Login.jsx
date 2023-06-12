import {Box, Button, Stack} from "@mui/material";
import {AccountCircle, Lock} from "../icons";
import IconTextField from "./IconTextField";

export default function Login () {
  return (
    <Stack width={1} gap={2}>
      {/*TODO увеличить бордер, сделать по ярче*/}
      <IconTextField placeholder="Логин" icon={<AccountCircle/>}/>
      <IconTextField placeholder="Пароль" type="password" icon={<Lock/>}/>
      <Box gap={4} display="flex">
        <Button fullWidth variant="contained" color="secondary" href="/account/register">Регистрация</Button>
        <Button fullWidth variant="contained">Войти</Button>
      </Box>
    </Stack>
  );
}