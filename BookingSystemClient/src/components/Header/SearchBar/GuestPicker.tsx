import { useRef, useState } from "react";
import useClickOutside from "../../../hooks/useClickOutside";

export default function GuestPicker() {
  const [isOpen, setIsOpen] = useState(false);

  const [adults, setAdults] = useState(0);
  const [children, setChildren] = useState(0);
  const [infants, setInfants] = useState(0);
  const [pets, setPets] = useState(0);

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
          <div className="absolute right-0 top-full z-10 mt-4 p-6 w-85 rounded-2xl bg-white shadow-lg">
            <div className="space-y-6">
              <div className="flex items-center justify-between">
                <div>
                  <div className="font-medium">Adults</div>
                  <div className="text-sm text-gray-500">Ages 18 or above</div>
                </div>
                <div className="flex items-center gap-3">
                  <button
                    type="button"
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all"
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                    </svg>
                  </button>
                  <span>{adults}</span>
                  <button
                    type="button"
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all"
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M8.75 3.75a.75.75 0 0 0-1.5 0v3.5h-3.5a.75.75 0 0 0 0 1.5h3.5v3.5a.75.75 0 0 0 1.5 0v-3.5h3.5a.75.75 0 0 0 0-1.5h-3.5v-3.5Z" />
                    </svg>
                  </button>
                </div>
              </div>
              <div className="flex items-center justify-between">
                <div>
                  <div className="font-medium">Children</div>
                  <div className="text-sm text-gray-500">Ages 2-17</div>
                </div>
                <div className="flex items-center gap-3">
                  <button
                    type="button"
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all"
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                    </svg>
                  </button>
                  <span>{children}</span>
                  <button
                    type="button"
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all"
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M8.75 3.75a.75.75 0 0 0-1.5 0v3.5h-3.5a.75.75 0 0 0 0 1.5h3.5v3.5a.75.75 0 0 0 1.5 0v-3.5h3.5a.75.75 0 0 0 0-1.5h-3.5v-3.5Z" />
                    </svg>
                  </button>
                </div>
              </div>
              <div className="flex items-center justify-between">
                <div>
                  <div className="font-medium">Infants</div>
                  <div className="text-sm text-gray-500">Under 2</div>
                </div>
                <div className="flex items-center gap-3">
                  <button
                    type="button"
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all"
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                    </svg>
                  </button>
                  <span>{infants}</span>
                  <button
                    type="button"
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all"
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M8.75 3.75a.75.75 0 0 0-1.5 0v3.5h-3.5a.75.75 0 0 0 0 1.5h3.5v3.5a.75.75 0 0 0 1.5 0v-3.5h3.5a.75.75 0 0 0 0-1.5h-3.5v-3.5Z" />
                    </svg>
                  </button>
                </div>
              </div>
              <div className="flex items-center justify-between">
                <div>
                  <div className="font-medium">Pets</div>
                  <div className="text-sm text-gray-500">
                    Bringing a service animal?
                  </div>
                </div>
                <div className="flex items-center gap-3">
                  <button
                    type="button"
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all"
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                    </svg>
                  </button>
                  <span>{pets}</span>
                  <button
                    type="button"
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all"
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M8.75 3.75a.75.75 0 0 0-1.5 0v3.5h-3.5a.75.75 0 0 0 0 1.5h3.5v3.5a.75.75 0 0 0 1.5 0v-3.5h3.5a.75.75 0 0 0 0-1.5h-3.5v-3.5Z" />
                    </svg>
                  </button>
                </div>
              </div>
            </div>
          </div>
        )}
      </div>
    </>
  );
}
