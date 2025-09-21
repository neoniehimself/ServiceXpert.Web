export function initUserPickerSearchbox() {
    const debounceDelay = 300;
    let debounceTimeout;

    $(document).on('keyup', '.partial-user-picker-searchbox', function (e) {
        clearTimeout(debounceTimeout);

        const input = $(this);
        const wrapper = input.closest('.position-relative');
        const dataList = $(`#${input.attr('list')}`);
        const hiddenInput = wrapper.find('input[type="hidden"]');
        const spinner = wrapper.find('#spinner-' + wrapper.data('instance-id'));
        const searchQuery = input.val().trim();

        // Skip AJAX if user already selected a valid option
        if (input.data('selected') === true && hiddenInput.val()) {
            return;
        }

        // Ignore non-character keys (arrows, tab, etc.)
        if (e.key.length > 1 && e.key !== 'Backspace' && e.key !== 'Delete') {
            return;
        }

        debounceTimeout = setTimeout(() => {
            if (!searchQuery) {
                dataList.empty();
                hiddenInput.val('');
                spinner.addClass('d-none');
                return;
            }

            spinner.removeClass('d-none');

            $.ajax({
                url: '/Users/SearchUserByName',
                method: 'GET',
                data: { searchQuery },
                success: function (response) {
                    dataList.empty();
                    response.userProfiles.forEach(userProfile => {
                        dataList.append(
                            `<option data-id="${userProfile.id}" value="${userProfile.firstNameLastName}"></option>`
                        );
                    });
                },
                error: function () {
                    dataList.empty();
                },
                complete: function () {
                    spinner.addClass('d-none');
                }
            });
        }, debounceDelay);
    });

    // Handle selection from datalist
    $(document).on('change', '.partial-user-picker-searchbox', function () {
        const input = $(this);
        const wrapper = input.closest('.position-relative');
        const dataList = $(`#${input.attr('list')}`);
        const hiddenInput = wrapper.find('input[type="hidden"]');
        const typedValue = input.val();

        const matchedOption = dataList.find(`option[value="${typedValue}"]`);

        if (matchedOption.length > 0) {
            hiddenInput.val(matchedOption.data('id'));
            input.data('selected', true); // Mark as selected
            dataList.empty(); // Clear stale suggestions
        } else {
            hiddenInput.val('');
            input.data('selected', false);
        }
    });

    // Reset "selected" flag when user types again
    $(document).on('keydown', '.partial-user-picker-searchbox', function () {
        $(this).data('selected', false);
    });
}
