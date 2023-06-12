import {Box, Button} from "@mui/material";
import {ReactComponent as BrandSvg} from "../../assets/brand.svg";

export default function Brand() {
  return (
    <Button href="/" variant="text" color="primary" sx={{height: 80, padding: 0}}>
      <Box display="flex" justifyContent="center" alignItems="center">
        <BrandSvg/>
      </Box>
    </Button>
  );
}