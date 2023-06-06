import {Box, Button, InputAdornment, Stack, TextField} from "@mui/material";
import {AccountCircle, Lock} from "../../components/Icons";

export default function Login () {
  return (
    <Stack width={1} gap={2}>
      {/*TODO увеличить бордер, сделать по ярче*/}
      <TextField placeholder="Логин" InputProps={{startAdornment: (<InputAdornment position="start"><AccountCircle/></InputAdornment>)}}/>
      <TextField placeholder="Пароль" type="password" InputProps={{startAdornment: (<InputAdornment position="start"><Lock/></InputAdornment>)}}/>
      <Box gap={4} display="flex">
        <Button fullWidth href="/account/register">Регистрация</Button>
        <Button fullWidth variant="contained">Войти</Button>
      </Box>
    </Stack>
  );
}