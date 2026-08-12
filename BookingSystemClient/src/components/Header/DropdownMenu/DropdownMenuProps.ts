import type { ReactNode } from "react";
import type DropdownMenuItem from "./DropdownMenuItem";

export default interface DropdownMenuProps {
    trigger: ReactNode;
    items: DropdownMenuItem[];
}