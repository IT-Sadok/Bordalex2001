import { useEffect, useRef, useState } from "react";
import type DropdownMenuProps from "./DropdownMenuProps";
import { Link } from "react-router";

export default function DropdownMenu({ trigger, items }: DropdownMenuProps) {
  const [isOpen, setIsOpen] = useState(false);
  const dropdownRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  return (
    <>
      <div ref={dropdownRef} className="relative flex">
        <button
          type="button"
          onClick={() => setIsOpen((prev) => !prev)}
          className="bg-gray-200 py-2 px-2 rounded-2xl hover:bg-gray-300 transition-all"
        >
          {trigger}
        </button>

        {isOpen && (
          <div className="absolute right-0 z-50 mt-10 w-48 rounded-xl border border-gray-200 bg-white py-2 shadow-lg">
            {items.map((item, index) => (
              <Link
                key={index}
                to={item.to || "#"}
                onClick={() => {
                  setIsOpen(false);
                  item.onClick?.();
                }}
                className="block px-4 py-2 text-sm hover:bg-gray-100"
              >
                {item.label}
              </Link>
            ))}
          </div>
        )}
      </div>
    </>
  );
}
