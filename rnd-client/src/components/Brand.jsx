import {Box} from "@mui/material";
import {ReactComponent as BrandSvg} from "../svg/brand.svg";

export default function Brand() {
  return (
    <Box height={80} width={1} gap={1.5} display="flex" justifyContent="center" alignItems="center">
      <BrandSvg/>
    </Box>
  );
}