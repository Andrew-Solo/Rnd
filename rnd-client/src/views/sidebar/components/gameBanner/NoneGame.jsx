import {Box, Button, Typography} from "@mui/material";

export default function NoneGame () {
  return (
    <Box height={170} padding={1} gap={1} ml={-2} display="flex" justifyContent="center" alignItems="center" flexDirection="column" sx={{background: "rgba(255, 255, 255, 0.2)"}}>
      <Typography>
        Нет игр
      </Typography>
      <Button size="large" href="/app/games/@new">
        Создать
      </Button>
    </Box>
  );
}