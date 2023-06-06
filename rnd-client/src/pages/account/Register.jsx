import {Box, Button, InputAdornment, Stack, TextField} from "@mui/material";
import {AccountCircle, Lock, Mail} from "../../components/Icons";

export default function Register () {
  return (
    <Stack width={1} gap={2}>
      {/*TODO увеличить бордер, сделать по ярче*/}
      <TextField placeholder="Логин" InputProps={{startAdornment: (<InputAdornment position="start"><AccountCircle/></InputAdornment>)}}/>
      <TextField placeholder="Почта" InputProps={{startAdornment: (<InputAdornment position="start"><Mail/></InputAdornment>)}}/>
      <TextField placeholder="Пароль" type="password" InputProps={{startAdornment: (<InputAdornment position="start"><Lock/></InputAdornment>)}}/>
      <TextField placeholder="Повтор пароля" type="password" InputProps={{startAdornment: (<InputAdornment position="start"><Lock/></InputAdornment>)}}/>
      <Box gap={4} display="flex">
        <Button fullWidth href="/account/login">Вход</Button>
        <Button fullWidth variant="contained">Регистрация</Button>
      </Box>
    </Stack>
  );
}