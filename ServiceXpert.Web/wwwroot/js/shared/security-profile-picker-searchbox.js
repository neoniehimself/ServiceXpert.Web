export function initSecurityProfilePickerSearchbox() {
    $(document).ready(function () {
        $("[id^='security-profile-picker-searchbox-']").each(function () {
            const container = $(this);
            const input = container.find("input[type='text']");
            const spinner = container.find("[id^='spinner-']");
            const list = container.find("[id^='result-list-']");
            const hidden = container.find("input[type='hidden']");

            let typingTimer;
            const typingDelay = 400; // Debounce

            input.on("keyup", function () {
                clearTimeout(typingTimer);
                const name = $(this).val().trim();

                // Reset hidden field when typing
                hidden.val("");

                if (!name) {
                    list.addClass("d-none").empty();
                    return;
                }

                // Show spinner
                spinner.removeClass("d-none");

                typingTimer = setTimeout(function () {
                    $.ajax({
                        url: "/Security/Users/Profiles/SearchProfileByName",
                        type: "GET",
                        data: { name },
                        success: function (response) {
                            list.empty();
                            if (response && response.profiles && response.profiles.length) {
                                response.profiles.forEach((profile, index) => {
                                    const isLast = index === response.profiles.length - 1;
                                    list.append(
                                        `<li class="list-group-item list-group-item-action ${isLast ? 'mb-3' : ''}" data-id="${profile.id}">${profile.firstNameLastName}</li>`
                                    );
                                });
                                list.removeClass("d-none");
                            } else {
                                list.addClass("d-none");
                            }
                        },
                        error: function () {
                            list.addClass("d-none").empty();
                            console.error("Error fetching profiles.");
                        },
                        complete: function () {
                            spinner.addClass("d-none");
                        }
                    });
                }, typingDelay);
            });

            // Handle selection
            list.on("click", "li", function () {
                const selectedName = $(this).text();
                const selectedId = $(this).data("id");

                input.val(selectedName);
                hidden.val(selectedId);
                list.addClass("d-none");
                spinner.addClass("d-none");
            });

            // Reset hidden field if typed value not selected
            input.on("blur", function () {
                const typedVal = $(this).val();
                const selectedId = hidden.val();

                if (typedVal && !selectedId) {
                    input.val("");
                    hidden.val("");
                }
            });

            // Close list when clicking outside
            $(document).click(function (e) {
                if (!$(e.target).closest(container).length) {
                    list.addClass("d-none");
                }
            });
        });
    });
}
