import { useRef, useState } from "react";
import useClickOutside from "../../../hooks/useClickOutside";

export default function GuestPicker() {
  const [isOpen, setIsOpen] = useState(false);

  const guestPickerRef = useRef<HTMLDivElement>(null);

  useClickOutside(guestPickerRef, () => {
    setIsOpen(false);
  });

  return (
    <>
      <div ref={guestPickerRef} className="relative">
        <button
          type="button"
          onClick={() => setIsOpen(!isOpen)}
          className="rounded-2xl px-3 py-2 text-left hover:bg-gray-100"
        >
          <div className="text-xs font-semibold">Who</div>
          <div className="text-sm text-gray-500">Add guests</div>
        </button>

        {isOpen && (
          <div className="absolute right-0 top-full z-10 mt-2 p-6 w-80 rounded-2xl bg-white shadow-lg">
            <p className="mb-3 text-sm font-semibold">Who is coming?</p>
          </div>
        )}
      </div>
    </>
  );
}
