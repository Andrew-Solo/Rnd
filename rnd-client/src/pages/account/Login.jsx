import {Box, Button, Stack} from "@mui/material";
import {AccountCircle, Lock} from "../../components/icons";
import IconTextField from "../../components/IconTextField";

export default function Login () {
  return (
    <Stack width={1} gap={2}>
      {/*TODO увеличить бордер, сделать по ярче*/}
      <IconTextField placeholder="Логин" icon={<AccountCircle/>}/>
      <IconTextField placeholder="Пароль" type="password" icon={<Lock/>}/>
      <Box gap={4} display="flex">
        <Button fullWidth href="/account/register">Регистрация</Button>
        <Button fullWidth variant="contained">Войти</Button>
      </Box>
    </Stack>
  );
}