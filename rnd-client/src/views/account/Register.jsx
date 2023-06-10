import {Box, Button, Stack} from "@mui/material";
import {AccountCircle, Lock, Mail} from "../../components/icons";
import IconTextField from "../../components/ui/IconTextField";

export default function Register () {
  return (
    <Stack width={1} gap={2}>
      {/*TODO увеличить бордер, сделать по ярче*/}
      <IconTextField placeholder="Логин" icon={<AccountCircle/>}/>
      <IconTextField placeholder="Почта" icon={<Mail/>}/>
      <IconTextField placeholder="Пароль" type="password" icon={<Lock/>}/>
      <IconTextField placeholder="Повтор пароля" type="password" icon={<Lock/>}/>
      <Box gap={4} display="flex">
        <Button fullWidth variant="contained" color="secondary" href="/account/login">Вход</Button>
        <Button fullWidth variant="contained">Регистрация</Button>
      </Box>
    </Stack>
  );
}